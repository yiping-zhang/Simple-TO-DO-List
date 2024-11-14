import { Injectable } from '@angular/core';
import { NewItem } from '../../models/requests/new-item';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ToDoItem } from '../../models/to-do-item';

export interface Todo {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class ToDoService {
  private apiUrl = 'https://localhost:5001/to-do-items';

  constructor(private http: HttpClient) {}

  getTodoItems(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(this.apiUrl);
  }

  addTodo(newTodo: NewItem): Observable<number> {
    return this.http.post<number>(this.apiUrl, newTodo);
  }

  deleteTodo(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
