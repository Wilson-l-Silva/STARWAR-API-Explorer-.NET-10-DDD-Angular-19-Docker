import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Planet } from '../models/planet.model';
import { Character } from '../models/character.model';
import { environment } from '../../environments/environment';

// providedIn: 'root' -> Angular cria uma ÚNICA instância deste serviço para toda a aplicação
// (equivalente ao "Scoped/Singleton" do .NET, mas no client-side).
@Injectable({
  providedIn: 'root',
})
export class StarWarsService {
  // Injeção de dependência via construtor: o Angular injeta o HttpClient automaticamente.
  constructor(private http: HttpClient) { }

  // Busca a lista paginada de planetas na nossa API .NET
  getPlanets(page: number = 1): Observable<Planet[]> {
    return this.http.get<Planet[]>(`${environment.apiUrl}/planets?page=${page}`);
  }

  // Busca um planeta específico por id
  getPlanetById(id: number): Observable<Planet> {
    return this.http.get<Planet>(`${environment.apiUrl}/planets/${id}`);
  }

  // Busca a lista paginada de personagens
  getCharacters(page: number = 1): Observable<Character[]> {
    return this.http.get<Character[]>(`${environment.apiUrl}/characters?page=${page}`);
  }

  // Busca um personagem específico por id
  getCharacterById(id: number): Observable<Character> {
    return this.http.get<Character>(`${environment.apiUrl}/characters/${id}`);
  }
}
