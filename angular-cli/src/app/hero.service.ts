import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
//
import { Hero } from './heroes/hero';
import { HEROES } from './heroes/mock-heroes';
import { MessageService } from './message.service';

@Injectable()
export class HeroService {

  constructor(private messageService: MessageService) { }

  getHeroes(): Observable<Hero[]> {
    // сообщение об извлечении данных о героях
    this.messageService.add('HeroService: fetched heroes');
    //
    return of(HEROES);
  }

  getHeroesSlowly(): Observable<Hero[]> {
    return new Observable<Hero[]>(
      // of => setTimeout(
      //   () => of(HEROES),
      //   2000
      // )      
    );
  }
}
