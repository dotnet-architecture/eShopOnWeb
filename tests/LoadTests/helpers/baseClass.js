export class BaseClass {
    constructor(endpoint){
        this.url = endpoint.concat('/api/')
        this.result
    }

    checkResponseStatus(expectedStatus = 200){
        if (this.result.status != expectedStatus){
            console.error(`GET ${this.url} failed with HTTP code ${this.result.status}`);
        }
    }

    getResult(){
        return this.result;
    }
}