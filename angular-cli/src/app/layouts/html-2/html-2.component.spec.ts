import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Html2Component } from './html-2.component';

describe('Html2Component', () => {
  let component: Html2Component;
  let fixture: ComponentFixture<Html2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Html2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Html2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
