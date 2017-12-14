import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Wallet } from './wallet';

@Component({
    selector: 'my-app',
    templateUrl: './app.component.html',
    providers: [DataService]
})

export class AppComponent implements OnInit {

    wallet: Wallet = new Wallet();   // изменяемый кошелёк
    wallets: Wallet[];               // массив кошельков
    tableMode: boolean = true;       // табличный режим

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadWallets();    // загрузка данных при старте компонента  
    }

    // получаем данные через сервис
    loadWallets() {
        this.dataService.getWallets()
            .subscribe((data: Wallet[]) => this.wallets = data);
    }

    // сохранение данных
    save() {
        if (this.wallet.Id == null) {
            this.dataService.createWallet(this.wallet)
                .subscribe((data: Wallet) => this.wallets.push(data));
        } else {
            this.dataService.updateWallet(this.wallet)
                .subscribe((data: Wallet) => this.loadWallets());
        }
        this.cancel();
    }

    editWallet(p: Wallet) {
        this.wallet = p;
    }
    cancel() {
        this.wallet = new Wallet();
        this.tableMode = true;
    }
    delete(p: Wallet) {
        this.dataService.deleteWallet(p.Id)
            .subscribe((data: Wallet) => this.loadWallets());
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
}