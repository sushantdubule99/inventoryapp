import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { InventoryModel } from '../Class/inventory-model';

@Service()
export class InventoryService {
    private http =inject(HttpClient);

    private apiUrl = 'https://localhost:7147/api/Inventory';

    getAll(){
        return this.http.get<InventoryModel[]>(this.apiUrl);
    }
     
    getById(id:number){
        return this.http.get<InventoryModel>(`${this.apiUrl}/${id}`);
    } 

    create(data: InventoryModel){
        return this.http.post<InventoryModel>(this.apiUrl, data);
    } 

    update(id: number, data: InventoryModel){
        return this.http.put<InventoryModel>(`${this.apiUrl}/${id}`, data);
    }

    delete(id: number){
        return this.http.delete(`${this.apiUrl}/${id}`);
    }

}
