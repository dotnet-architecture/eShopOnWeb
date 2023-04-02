import http from 'k6/http';
import { sleep, check, group } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

const LOG_PREFIX = '[Home] '

export class Home {
  constructor(baseUrl, auth){
    this.url = baseUrl;
    this.auth = auth;
  }

  load() {
    const params = {
      headers: {
        Authorization: this.auth,
      },
    };
    
    let response;   
    group('home', () => {
      response = http.get(this.url, params)

      check(response, {
        "success status code": (res) => res.status == 200,
        "response time": (res) => res.timings.duration < 1500

      });  
    });

    if (response.status != 200){
      console.error(LOG_PREFIX, `Error loading home page. Status code: ${response.status}`);
      return false;
    }

    let doc = response.html()

    this.requestVerificationToken = doc.find('form[action="/Basket"]')
      .first()  
      .children('input[name="__RequestVerificationToken"]')
      .attr('value');
    console.debug(LOG_PREFIX, 'RequestVerificationToken: ', this.requestVerificationToken);

    this.products = [];
    
    doc.find('form[action="/Basket"]').each((idx, form) => {
      const product = { 
        id : form.querySelector('input[name="id"]').getAttribute('value')
      }
      this.products.push(product);
    });

    console.debug(LOG_PREFIX, 'Products: ', this.products);

    return true;
  }

  /**
   * Add a random item to the basket
   */
  addToBasket() {

    if (!this.products){
      console.warn(LOG_PREFIX, `No products found to add to the basket`);
      return false;
    }

    let body = Object.assign(this.products[randomIntBetween(0, this.products.length -1)]);
    body.__RequestVerificationToken = this.requestVerificationToken;
    
    const params = {
      headers: {
        Authorization: this.auth,
      },
    };
    
    let response;
    group('add to basket', () => {
      response = http.post(this.url.concat('/Basket'), body, params);

      check(response, {
        "success status code": (res) => res.status == 200,
        "response time": (res) => res.timings.duration < 1500
      });
    })

    if (response.status != 200){
      console.error(LOG_PREFIX, `Error adding a product to the basket. Status code: ${response.status}`);
      return false;
    }
    
    return true;
  }
}
