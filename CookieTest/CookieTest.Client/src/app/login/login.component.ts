import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User = new User(0, '', '', '', false);

  constructor(private accountSvc: AccountService, private router: Router) { }

  ngOnInit() {
  }

  logIn() {
    this.accountSvc.logIn(this.user).subscribe(
      (res: any) => {
        localStorage.setItem("token", res.token);
        this.router.navigate(['/some']);
      }
    );
  }
}
