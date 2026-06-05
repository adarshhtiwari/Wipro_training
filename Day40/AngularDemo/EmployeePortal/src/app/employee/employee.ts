import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-employee',
  standalone: true,
  templateUrl: './employee.html',
  styleUrl: './employee.css'
})
export class EmployeeComponent implements OnInit {

  employeeName: string = 'Adarsh';
  employeeId: number = 101;

  constructor() {
    console.log('Employee Component Constructor Called');
  }

  ngOnInit(): void {
    console.log('Employee Component Loaded');
  }
}