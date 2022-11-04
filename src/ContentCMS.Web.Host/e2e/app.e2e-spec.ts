import { ContentCMSTemplatePage } from './app.po';

describe('ContentCMS App', function() {
  let page: ContentCMSTemplatePage;

  beforeEach(() => {
    page = new ContentCMSTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
