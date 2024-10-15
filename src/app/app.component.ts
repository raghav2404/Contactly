
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import { Contact } from '../models/contact.model';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HttpClientModule,AsyncPipe,FormsModule,ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  
  http = inject(HttpClient);

 contactForm = new FormGroup({

    name :new FormControl<string>(''),
    email:new FormControl<string | null>(null),
    phone :new FormControl<string | null>(''),
    favourite:new FormControl<boolean>(false)

 })

  contacts$ = this.getContacts();

  onFormSubmit()
  {
    var addContactRequest = 
    {
      name :this.contactForm.value.name,
      email:this.contactForm.value.email,
      phone:this.contactForm.value.phone,
      favourite:this.contactForm.value.favourite
    }

    this.http.post('https://localhost:7065/api/Contacts',addContactRequest)
    .subscribe(
      {
  
     next:(value) => {
      this.contacts$ = this.getContacts();
      this.contactForm.reset(); 
     }

    });
  }

    onDelete(id: string)
    {
         this.http.delete(`https://localhost:7065/api/Contacts/${id}`).subscribe(
          {
            next:(value) => {
              alert('Item deleted')
              this.contacts$ = this.getContacts();
            }
          }
         )
    }
  


  private getContacts(): Observable<Contact[]>
  {
   return this.http.get<Contact[]>('https://localhost:7065/api/Contacts');
  }


}
