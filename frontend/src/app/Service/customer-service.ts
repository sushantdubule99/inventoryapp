import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { CustomerModel } from '../Class/customer-model';

@Service()
export class CustomerService {
    private http = inject(HttpClient);

    private apiUrl = 'https://localhost:7147/api/Customer';

    getAll(){
        return this.http.get<CustomerModel[]>(this.apiUrl);
    } 
 
    getById(id:number){
        return this.http.get<CustomerModel>(`${this.apiUrl}/${id}`);
    } 

    create(data:CustomerModel){
        return this.http.post<CustomerModel>(this.apiUrl, data);
    } 

    update(id:number ,data:CustomerModel){
        return this.http.put<CustomerModel>(`${this.apiUrl}/${id}`, data);
    } 

    delete(id:number){
        return this.http.delete(`${this.apiUrl}/${id}`);
    }

}
