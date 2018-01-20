import { Injectable } from '@angular/core';
import { Hero } from './heroes/hero';
import { HEROES } from './heroes/mock-heroes';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class HeroService {

  constructor() { }

  getHeroes(): Observable<Hero[]> {
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
