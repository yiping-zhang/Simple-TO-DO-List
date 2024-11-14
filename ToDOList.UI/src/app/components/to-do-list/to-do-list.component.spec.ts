import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { ToDoListComponent } from './to-do-list.component';
import {ToDoService} from './to-do.service';
import {ToDoItem} from '../../models/to-do-item';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';

describe('ToDoListComponent', () => {
  let component: ToDoListComponent;
  let fixture: ComponentFixture<ToDoListComponent>;
  let toDoServiceSpy: jasmine.SpyObj<ToDoService>;

  const mockToDoItems: ToDoItem[] = [
    { id: 1, name: 'Sample ToDo 1' },
    { id: 2, name: 'Sample ToDo 2' }
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('ToDoService', ['getTodoItems', 'addTodo', 'deleteTodo']);

    await TestBed.configureTestingModule({
      declarations: [ToDoListComponent],
      imports: [ReactiveFormsModule],
      providers: [{ provide: ToDoService, useValue: spy }]
    }).compileComponents();

    toDoServiceSpy = TestBed.inject(ToDoService) as jasmine.SpyObj<ToDoService>;
    fixture = TestBed.createComponent(ToDoListComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize toDoItems on ngOnInit', fakeAsync(() => {
    toDoServiceSpy.getTodoItems.and.returnValue(of(mockToDoItems));

    component.ngOnInit();
    tick();

    expect(component.toDoItems).toEqual(mockToDoItems);
    expect(toDoServiceSpy.getTodoItems).toHaveBeenCalled();
  }));
});
