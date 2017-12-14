export class Wallet {
    constructor(
        public Id?: number, 
        public UserId?: number,  // привязка кошелька к пользователю      
        public Balance?: number  // баланс
    ) { }
}