import { Selector } from 'testcafe';

fixture`CounterPage`
  .page`https://localhost:5011/`;

test('Counter Should Count even via js interop', async t => {
  await t
    .click(Selector('a').withText('Counter'));

  const Button1 = Selector('[data-qa="Counter1"]').find('button');
  const Button2 = Selector('[data-qa="Counter2"]').find('button');
  const Button3 = Selector('[data-qa="JsInterop"]');
  const CounterDisplay1 = Selector('[data-qa="Counter1"]').find('p');
  const CounterDisplay2 = Selector('[data-qa="Counter2"]').find('p');

  await t
    .expect(CounterDisplay1.textContent).eql("Current count: 3")
    .expect(CounterDisplay2.textContent).eql("Current count: 3")
    .click(Button1)
    .expect(CounterDisplay1.textContent).eql("Current count: 8")
    .expect(CounterDisplay2.textContent).eql("Current count: 8")
    .click(Button2)
    .expect(CounterDisplay1.textContent).eql("Current count: 13")
    .expect(CounterDisplay2.textContent).eql("Current count: 13")
    .click(Button1)
    .expect(CounterDisplay1.textContent).eql("Current count: 18")
    .expect(CounterDisplay2.textContent).eql("Current count: 18")
    .click(Button3)
    .expect(CounterDisplay1.textContent).eql("Current count: 25")
    .expect(CounterDisplay2.textContent).eql("Current count: 25");
});
