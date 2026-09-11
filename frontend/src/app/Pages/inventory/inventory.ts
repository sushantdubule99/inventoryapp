import { Component, inject, ChangeDetectorRef  } from '@angular/core';
import { InventoryService } from '../../Service/inventory-service';
import { InventoryModel } from '../../Class/inventory-model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-inventory',
  imports: [FormsModule,CommonModule],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css',
})
export class Inventory {
  private service = inject(InventoryService);
  private cdr = inject(ChangeDetectorRef);

  list: InventoryModel[] = [];

  obj = new InventoryModel();

  ngOnInit() {
    this.getAll();
  } 

  getAll() {
    this.service.getAll().subscribe({
      next: data => {
        console.log('GET DATA:', data);
        this.list = data;
       console.log('LIST:', this.list);},
      
        
      error: err => console.error(err)
    })
  } 

    save(){
      this.service.create(this.obj).subscribe({
        next: data => {
          console.log(data);
          this.getAll();
          this.reset();
          this.cdr.detectChanges();
        },
        error: err => console.error(err)
      })
    } 

    update(){
       console.log("Product ID:", this.obj.productId);
  console.log("Object:", this.obj);
      this.service.update(this.obj.productId,this.obj).subscribe({
        next: ()=>{
          this.getAll();
          this.reset();
          this.cdr.detectChanges();
        },
        error: err => console.error(err)
      })
    } 

    edit(item:InventoryModel){
      this.obj = Object.assign(new InventoryModel(), item);
    } 

    delete(id:number){
      if(!confirm('Are you sure to delete this record?')) return;
      this.service.delete(id).subscribe({
        next: ()=>{
          this.getAll();
          this.reset();
          this.cdr.detectChanges();
        },
        error: err => console.error(err)
      })
    } 

    reset(){
      this.obj = new InventoryModel();
    }
    
}
