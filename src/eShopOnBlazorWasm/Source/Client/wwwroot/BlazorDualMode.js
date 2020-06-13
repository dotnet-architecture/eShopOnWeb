function configureBlazor(applicationVersion) {
  const clientApplicationKey = "clientApplication";
  const executionSideKey = "executionSide";

  const clientApplicationValue = localStorage.getItem(clientApplicationKey);
  const executionSideValue = localStorage.getItem(executionSideKey);

  const clientLoaded = clientApplicationValue === applicationVersion;
  window.TimeWarp = {
    applicationVersion,
    clientApplicationKey,
    clientLoaded,
    executionSideKey,
    loadClient
  };
  if (executionSideValue === null) {
    localStorage.setItem(window.TimeWarp.executionSideKey, "To force a side set this to client/server");
  }

  const clientSideBlazorScript = "_framework/blazor.webassembly.js";
  const serverSideBlazorScript = "_framework/blazor.server.js";
  const executionSides = { client: "client", server: "server" };

  if (executionSideValue === executionSides.client) {
    source = clientSideBlazorScript;
  } else if (executionSideValue === executionSides.server) {
    source = serverSideBlazorScript;
  } else {
    source = clientLoaded ? clientSideBlazorScript : serverSideBlazorScript;
  }

  console.log(`Using script: ${source}`);

  var blazorScript = document.createElement("script");
  blazorScript.setAttribute("src", source);
  document.body.appendChild(blazorScript);
}

function loadClient() {
  if (!window.TimeWarp.clientLoaded) {
    localStorage.setItem(window.TimeWarp.clientApplicationKey, window.TimeWarp.applicationVersion);
    var iframe = document.createElement("iframe");
    iframe.setAttribute("id", "loaderFrame");
    iframe.setAttribute("style", "width:0; height:0; border:0; border:none");
    document.body.appendChild(iframe);
    const iframeSource = window.location.href;
    iframe.setAttribute("src", iframeSource);
  }
}
