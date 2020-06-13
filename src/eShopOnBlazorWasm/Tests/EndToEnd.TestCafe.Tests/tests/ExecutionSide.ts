import { ClientFunction, Selector } from "testcafe";

import { getLocalStorage, setLocalStorage } from "../util/LocalStorage";

enum ExecutionSide {
  Server = "server",
  Client = "client"
}

fixture`HomePage`.page`https://localhost:5011/`;

test("Should Load Server Side", async t => {
  const defaultExecutionSideValue = "To force a side set this to client/server";
  const executionSideKey = "executionSide";

  let executionSide = await getLocalStorage(executionSideKey);
  await t.expect(executionSide).eql(defaultExecutionSideValue);

  await setLocalStorage(executionSideKey, ExecutionSide.Server);
  await t.eval(() => location.reload());

  executionSide = await getLocalStorage(executionSideKey);
  await t.expect(executionSide).eql(ExecutionSide.Server);

  const blazorLocation = Selector('[data-qa="BlazorLocation"]');

  await t.expect(blazorLocation.textContent).eql("Server Side");

  await ValidateInitialization(t);
});

test("Should Load Client Side", async t => {
  const defaultExecutionSideValue = "To force a side set this to client/server";
  const executionSideKey = "executionSide";

  let executionSide = await getLocalStorage(executionSideKey);
  await t.expect(executionSide).eql(defaultExecutionSideValue);

  await setLocalStorage(executionSideKey, ExecutionSide.Client);
  await t.eval(() => location.reload(true));

  executionSide = await getLocalStorage(executionSideKey);
  await t.expect(executionSide).eql(ExecutionSide.Client);

  const blazorLocation = Selector('[data-qa="BlazorLocation"]');

  const timeToLetClientSideLoad = 5000; // will originally render server side.  5 sec should be enough to switch.
  await t
    .wait(timeToLetClientSideLoad)
    .expect(blazorLocation.textContent)
    .eql("Client Side");

  await ValidateInitialization(t);
});

const ValidateInitialization = async(t: TestController): Promise<void> => {
  const hasBlazorState = ClientFunction(() =>
    window.hasOwnProperty("BlazorState")
  );
  const hasInitializeJavaScriptInterop = ClientFunction(() =>
    window.hasOwnProperty("InitializeJavaScriptInterop")
  );
  const hasReduxDevToolsFactory = ClientFunction(() =>
    window.hasOwnProperty("ReduxDevToolsFactory")
  );
  const hasReduxDevTools = ClientFunction(() =>
    window.hasOwnProperty("reduxDevTools")
  );
  const hasJsonRequestHandler = ClientFunction(() =>
    window.hasOwnProperty("jsonRequestHandler")
  );
  await t
    .expect(hasBlazorState())
    .ok()
    .expect(hasInitializeJavaScriptInterop())
    .ok()
    .expect(hasReduxDevToolsFactory())
    .ok()
    .expect(hasReduxDevTools())
    .ok()
    .expect(hasJsonRequestHandler())
    .ok();
}
