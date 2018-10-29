#!/bin/bash

## Please login prior running the script ##
## az login ##

# variables
$servername='eshop-dbserver'
$resgroup='eshop-dev-rscgp'
$location='eastus'

$adminidentitylogin='eShopCatalog'
$identitypassword='e1Shop8Identity3Pwd'

$admincataloglogin='eShopCatalog'
$catalogpassword='e1Shop8Identity3Pwd'

$dbIdentity='eShop.Identity'
$dbCatalog='eShop.Catalog'

# The ip address range that you want to allow to access your DB
$startip='0.0.0.0'
$endip='0.0.0.0'
$endip254='254.254.254.254'

"------------------- Clean up. -----------------"
# Delete existing resource group

## known issue https://feedback.azure.com/forums/281804-azure-resource-manager/suggestions/5875886-deleting-resource-group-doesn-t-work
## for now delete manually before running script
## $resourceGroup = az resource list --name $resgroup
## if ($resourceGroup.length -ne 0) 
## {	

##	    az group delete `
##  			--name $resgroup `
##  			--no-wait --yes
	
##  	az group wait `
##  			--name $resgroup `
##  			--deleted
## }


# Create a resource group
az group create `
		--name $resgroup `
		--location $location
			
# Create a logical server in the resource group

az sql server create `
				--name $servername `
				--resource-group $resgroup `
				--location $location `
				--admin-user $adminidentitylogin `
				--admin-password $identitypassword				


# az sql server update `
#				--name $servername `
#				--resource-group $resgroup `
#				--location $location `
#				--admin-user $adminidentitylogin `
#				--admin-password $identitypassword
	
"------------------- Step 1: successfully created sql server. -----------------"

# Create a database in the server with zone redundancy as true

az sql db create `
			--resource-group $resgroup `
			--server $servername `
			--name $dbIdentity `
			--service-objective S0 `		

az sql db create `
			--resource-group $resgroup `
			--server $servername `
			--name $dbCatalog `
			--service-objective S0 `	
			
"------------------- Step 2: successfully created database. -----------------"

# Configure a firewall rule for the server

							
az sql server firewall-rule create `
							--end-ip-address  $endip `
						    --name 'AllowAllWindowsAzureIps' `
						    --resource-group $resgroup `
						    --server $servername `
						    --start-ip-address $startip	

az sql server firewall-rule create `
							--end-ip-address $endip254 `
						    --name 'All' `
						    --resource-group $resgroup `
						    --server $servername `
						    --start-ip-address $startip

"------------------- Step 3: successfully configured firewall. ---------------"
	
# Update database and set zone redundancy as false

az sql db update `
			  --resource-group $resgroup `
			  --server $servername `
			  --name $dbIdentity `
			  
az sql db update `
			  --resource-group $resgroup `
			  --server $servername `
			  --name $dbCatalog `
	

"------------------- Complete. -----------------"
