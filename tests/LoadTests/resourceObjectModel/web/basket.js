import http from 'k6/http';
import { sleep, check, group } from 'k6';

const LOG_PREFIX = '[Basket] '

export class Basket {
  constructor(baseUrl, auth){
    this.url = baseUrl.concat('/Basket');
    this.auth = auth;
  }

  load() {
    const params = {
      headers: {
        Authorization: this.auth,
      },
    };
    
    let response;
    group('basket', () => {
      response = http.get(this.url, params);

      check(response, {
        "success status code": (res) => res.status == 200,
        "response time": (res) => res.timings.duration < 3000
      });
    });

    //console.log(response);

    let doc = response.html()

    this.requestVerificationToken = doc.find('form')
      .first()  
      .children('input[name="__RequestVerificationToken"]')
      .attr('value');
    console.debug('RequestVerificationToken: ', this.requestVerificationToken);


  }

  checkout() {

    if (!this.requestVerificationToken){
      console.warn(LOG_PREFIX, 'RequestVerificationToken not found from');
      return false;
    }

    const body = {
      __RequestVerificationToken: this.requestVerificationToken
    }
    
    const params = {
      headers: {
        Authorization: this.auth,
      },
    };
    
    let response;
    group('ckeckout', () => {
    
      response = http.post(this.url.concat('/Checkout'), body, params);

      check(response, {
        "success status code": (res) => res.status == 200,
        "response time": (res) => res.timings.duration < 3000
      });
    });

    return response.status == 200;
  }
}
