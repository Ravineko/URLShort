import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface AboutText {
    text:string
}

@Injectable({
  providedIn: 'root'
})
export class TextService {
    private apiUrl = 'https://localhost:44305/text';
    private updateUrl = 'https://localhost:44305/UpdateText';
  
    constructor(private http: HttpClient) {}
  
    getText(): Observable<AboutText> {
      return this.http.get<AboutText>(this.apiUrl);
    }
  
    setText(content: string): Observable<AboutText> {
      return this.http.post<AboutText>(this.updateUrl, { content });
    }
}
