import { Injectable } from '@angular/core';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Wallet } from './wallet';

@NgModule({    
    imports: [HttpModule]
})

@Injectable()
export class DataService {

    private url = "/Wallets";

    constructor(private http: HttpClient) {
    }

    getWallets() {
        return this.http.get(this.url);
    }

    createWallet(wallet: Wallet) {
        return this.http.post(this.url, wallet);
    }

    updateWallet(wallet: Wallet) {

        return this.http.put(this.url + '/' + wallet.Id, wallet);
    }

    deleteWallet(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}