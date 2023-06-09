import { Component, OnInit } from '@angular/core';
import { SomeService } from '../services/some.service';

@Component({
  selector: 'app-some',
  templateUrl: './some.component.html',
  styleUrls: ['./some.component.css']
})
export class SomeComponent implements OnInit {
  someData: string;

  constructor(private someSvc: SomeService) { }

  ngOnInit() {
  }

  getSomeData() {
    this.someSvc.getSomeData().subscribe(
      (res: any) => this.someData = res.message
    );
  }
}
