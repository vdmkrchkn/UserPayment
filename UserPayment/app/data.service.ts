import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Wallet } from './wallet';

@Injectable()
export class DataService {

    private url = "/api/wallets";

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