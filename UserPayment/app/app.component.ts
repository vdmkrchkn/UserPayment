import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Wallet } from './wallet';

@Component({
    selector: 'app',
    //templateUrl: '~/app/app.component.html',
    providers: [DataService],
    template: `<h1>Добро пожаловать {{name}}!</h1>                
                <label>Введите имя:</label>
                <input [(ngModel)]="name" placeholder="name">`
})

export class AppComponent {
    name = '';
}
//export class AppComponent implements OnInit {

//    wallet: Wallet = new Wallet();   // изменяемый кошелек
//    Wallets: Wallet[];               // массив кошельков
//    tableMode: boolean = true;       // табличный режим просмотра

//    constructor(private dataService: DataService) { }

//    ngOnInit() {
//        this.loadWallets();    // загрузка данных при старте компонента  
//    }
//    // получаем данные через сервис
//    loadWallets() {
//        this.dataService.getWallets()
//            .subscribe((data: Wallet[]) => this.Wallets = data);
//    }
//    // сохранение данных
//    save() {
//        if (this.wallet.Id == null) {
//            this.dataService.createWallet(this.wallet)
//                .subscribe((data: Wallet) => this.Wallets.push(data));
//        } else {
//            this.dataService.updateWallet(this.wallet)
//                .subscribe((data: Wallet)  => this.loadWallets());
//        }
//        this.cancel();
//    }
//    editWallet(p: Wallet) {
//        this.wallet = p;
//    }
//    cancel() {
//        this.wallet = new Wallet();
//        this.tableMode = true;
//    }
//    delete(p: Wallet) {
//        this.dataService.deleteWallet(p.Id)
//            .subscribe((data: Wallet) => this.loadWallets());
//    }
//    add() {
//        this.cancel();
//        this.tableMode = false;
//    }
//}