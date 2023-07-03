import { BookingWebTemplatePage } from './app.po';

describe('BookingWeb App', function() {
  let page: BookingWebTemplatePage;

  beforeEach(() => {
    page = new BookingWebTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
