import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CustomerModel } from '../../Class/customer-model';
import { CustomerService } from '../../Service/customer-service';

@Component({
  selector: 'app-customer',
  imports: [FormsModule,CommonModule],
  templateUrl: './customer.html',
  styleUrl: './customer.css',
})
export class Customer {
  customerSrv = inject (CustomerService);
  
  customerObj = new CustomerModel();
  customers: CustomerModel[] = [];

  ngOnInit() {
    this.getAllCustomers();
  }

  getAllCustomers() {
   this.customerSrv.getAll().subscribe((data)=>{
     this.customers = data;
   }) 
  }

  saveCustomer() {
    this.customerSrv.create(this.customerObj).subscribe(()=>{
      alert("Customer saved successfully");
      this.customerObj = new CustomerModel(); // Reset the customerObj after saving
      this.getAllCustomers();
    })

  }

  updateCustomer() {
    this.customerSrv.update(this.customerObj.customerId,this.customerObj).subscribe(()=>{
     alert('Customer updated successfully');
     this.customerObj = new CustomerModel(); // Reset the customerObj after updating
     this.getAllCustomers();
    // Implement logic to update an existing customer using the 'customerObj'
    // For example, you can use a service to make an HTTP PUT request
    // and then re.fresh the customer list by calling 'getAllCustomers()'.
  })
}

  editCustomer(customer: CustomerModel) {
    // Implement logic to populate the 'customerObj' with the selected customer's data
    // so that it can be edited in the form.
    this.customerObj = { ...customer };
  } 

 deleteCustomer(customerId: number) {
  this.customerSrv.delete(customerId).subscribe({
    next: () => {
      alert('Customer deleted successfully');
      this.getAllCustomers();
    },
    error: (err) => {
      console.log(err);
      alert('Unable to delete customer');
    }
  });
}
}
