import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToDoService } from './to-do.service';
import { ToDoItem } from '../../models/to-do-item';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss'],
})
export class ToDoListComponent implements OnInit {
  toDoForm: FormGroup;
  toDoItems: ToDoItem[] = [];

  constructor(private formBuilder: FormBuilder, private toDoService: ToDoService) {
    this.toDoForm = this.formBuilder.group({
      name: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.toDoService.getTodos().subscribe((todos) => {
      this.toDoItems = todos;
    });
  }

  get name() {
    return this.toDoForm.get('name');
  }

  addTodo(): void {
    if (this.toDoForm.valid) {
      const newTodo = this.toDoForm.value;
      this.toDoService.addTodo(newTodo).subscribe((newId) => {
        this.toDoService.getTodos().subscribe((todos) => {
          this.toDoItems = todos; // Update the list with the fetched to-dos
        });

        this.toDoForm.reset(); // Reset the form after adding the todo
      });
    }
  }

  deleteTodo(id: number): void {
    this.toDoService.deleteTodo(id).subscribe(() => {
      this.toDoItems = this.toDoItems.filter((todo) => todo.id !== id);
    });
  }
}
