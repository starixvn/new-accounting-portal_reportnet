import { browser, element, by, ElementFinder, promise } from 'protractor';

export class PunterAnalysisPage {
  pause() {
    browser.pause();
  }

  isReady() {
    browser.wait(function () {
      const deferred = promise.defer();
      element(by.css('.spinner-container')).isDisplayed()
        .then(function (isPresent) {
          deferred.fulfill(!isPresent);
        });
      return deferred.promise;
    });
  }

  navigateTo() {
    return browser.get('/');
  }

  getMenu() {
    return element(by.css('nap-navbar'));
  }

  getHeaderText() {
    return element(by.css('.header-container')).getText();
  }

  getProductDropdownItems() {
    return element.all(by.css('.filter-container ngx-treeview-item'));
  }
}
