import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwiperxemthemanhComponent } from './swiperxemthemanh.component';

describe('SwiperxemthemanhComponent', () => {
  let component: SwiperxemthemanhComponent;
  let fixture: ComponentFixture<SwiperxemthemanhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SwiperxemthemanhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SwiperxemthemanhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
