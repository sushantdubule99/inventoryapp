import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable,tap } from 'rxjs';

interface LoginResponse {
  token: string;
}

@Service()
export class LoginService {
    
    private http = inject(HttpClient);

    private apiUrl = 'https://localhost:7147/api/Auth';
    login(data:any):Observable<LoginResponse>{
        return this.http.post<LoginResponse>(`${this.apiUrl}/login`,data).pipe(
            
            tap(response =>{
                localStorage.setItem('token', response.token);
            })
        );
    } 

    getToken(): string | null {
        return localStorage.getItem('token');
    } 

    logout(): void {
         localStorage.removeItem('token');
         localStorage.removeItem('userId');
         localStorage.removeItem('userName');
         localStorage.removeItem('email');
          localStorage.removeItem('role');
    } 

    isLoggedIn(): boolean {
        return this.getToken() !== null;
    }
}
