$FeatureName = "Catalog"
$RootNamespace = "eShopOnBlazorWasm"
$RequestName = "GetCatalogBrands"

$ApiFeaturePath = "..\Source\Api\Features\$FeatureName"
$ApiRequestPath = "$ApiFeaturePath\$RequestName"
$ServerPath = "..\Source\Server\Features\$FeatureName\$RequestName"
$ServerTestsPath = "..\Tests\Server.Integration.Tests\Features\$FeatureName\$RequestName"

New-Item -ItemType Directory -Force -Path $ApiRequestPath
New-Item -ItemType Directory -Force -Path $ServerPath
New-Item -ItemType Directory -Force -Path $ServerTestsPath

Move-Item "Api\*" -Destination $ApiRequestPath
Move-Item "Server\*" -Destination $ServerPath
Move-Item "Server.Tests\*" -Destination $ServerTestsPath
Move-Item "FeatureAnnotations.cs" -Destination $ApiFeaturePath