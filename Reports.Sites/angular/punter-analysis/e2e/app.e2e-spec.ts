import { PunterAnalysisPage } from './app.po';

describe('punter-analysis App', () => {
  let page: PunterAnalysisPage;

  beforeEach(() => {
    page = new PunterAnalysisPage();
    page.navigateTo();
  });

  describe('menu', () => {
    it('should exist', () => {
      expect(page.getMenu().isPresent()).toBeTruthy();
    });
  });

  describe('header-container', () => {
    it('should display page header "Fraud & Risk / Punter Analysis"', () => {
      const expected: any = 'Fraud & Risk / Punter Analysis';
      expect(page.getHeaderText()).toEqual(expected);
    });
  });

  describe('filter-container', () => {
    it('should have more than 10 product dropdown item', () => {
      page.isReady();
      const productDropdownItems = page.getProductDropdownItems();
      // page.pause();
      expect(productDropdownItems.count()).toBeGreaterThan(10);
    });
  });
});
