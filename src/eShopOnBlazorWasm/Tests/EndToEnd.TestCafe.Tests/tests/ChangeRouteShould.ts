import { ClientFunction, Selector } from 'testcafe';

fixture `CounterPage`
    .page `https://localhost:5011/`;
    
test('Change Route Should Change Route', async t => {
    await t
        .click(Selector('a').withText('Counter'));

    const changeRouteButton = Selector('[data-qa="ChangeRoute"]');
    const homeHeader = Selector('[data-qa="HomeHeader"]');

    await t
        .click(changeRouteButton)
        .expect(homeHeader.exists).ok();

});
