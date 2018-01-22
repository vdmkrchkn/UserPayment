import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Html1Component } from './html-1.component';

describe('Html1Component', () => {
  let component: Html1Component;
  let fixture: ComponentFixture<Html1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Html1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Html1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
