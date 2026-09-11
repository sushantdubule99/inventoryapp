import { Component, inject } from '@angular/core';
import { LoginService } from '../../Service/login-service';
import { Router } from '@angular/router';
import { LoginModel } from '../../Class/login-model';
import { FormsModule ,} from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule,],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private authService = inject(LoginService);
  private router = inject(Router); 

  loginData:LoginModel = {
    email: '',
    password: ''
  };

  errorMessage=''; 
  
  login(): void {
    this.errorMessage='';
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('JWT Token:',response.token);
          localStorage.setItem('token', response.token);

        this.router.navigate(['/inventory']);
        
      },
      error: (error) => {
        this.errorMessage='Invalid email or password';
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
