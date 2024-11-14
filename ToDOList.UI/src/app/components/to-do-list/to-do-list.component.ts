import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToDoService } from './to-do.service';
import { ToDoItem } from '../../models/to-do-item';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { switchMap, finalize } from 'rxjs/operators';

// @ts-ignore
@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss']
})
export class ToDoListComponent implements OnInit {
  toDoForm: FormGroup;
  toDoItems: ToDoItem[] = [];
  errorMessage: string = '';
  isLoading: boolean = false

  constructor(private formBuilder: FormBuilder, private toDoService: ToDoService ) {
    this.toDoForm = this.formBuilder.group({
      name: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.toDoService.getTodoItems().pipe(catchError(
      error => {
        this.errorMessage = "Failed to retrieve to do list";
        return of([]);
      })).subscribe({
      next: data => {
        this.toDoItems = data;
      }
    });
  }

  get name() {
    return this.toDoForm.get('name');
  }

  addTodo(): void {
    if (this.toDoForm.valid) {
      this.isLoading = true;
      const newTodo = this.toDoForm.value;

      this.toDoService.addTodo(newTodo).pipe(
        switchMap(() => this.toDoService.getTodoItems()),
        finalize(() => {
          this.isLoading = false;
          this.toDoForm.reset();
        })
      ).subscribe((todos) => {
        this.toDoItems = todos;
      });
    }
  }

  deleteTodo(id: number): void {
    this.isLoading = true;

    this.toDoService.deleteTodo(id).pipe(
      switchMap(() => this.toDoService.getTodoItems()),
      finalize(() => {
        this.isLoading = false;
        this.toDoForm.reset();
      })
    ).subscribe((todos) => {
      this.toDoItems = todos;
    });
  }
}
