import { Routes } from '@angular/router';
import { PlanetListComponent } from './components/planet-list/planet-list';

// Define que a rota raiz "/" renderiza a listagem de planetas.
export const routes: Routes = [
  { path: '', component: PlanetListComponent },
];
