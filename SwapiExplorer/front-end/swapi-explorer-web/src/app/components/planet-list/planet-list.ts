import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // fornece *ngFor, *ngIf, etc.
import { Planet } from '../../models/planet.model';
import { StarWarsService } from '../../services/star-wars';

@Component({
  selector: 'app-planet-list',
  standalone: true, // componente standalone: não precisa de NgModule
  imports: [CommonModule],
  templateUrl: './planet-list.html',
  styleUrl: './planet-list.scss',
})
export class PlanetListComponent implements OnInit {
  // Lista de planetas exibida no template
  planets: Planet[] = [];

  // Página atual da paginação da SWAPI
  currentPage = 1;

  // Flag de carregamento, usada para exibir um spinner/mensagem no template
  loading = false;

  // Mensagem de erro, exibida caso a chamada à API falhe
  errorMessage: string | null = null;

  constructor(
    private starWarsService: StarWarsService,
    private changeDetector: ChangeDetectorRef,
  ) { }

  // Hook de ciclo de vida do Angular: executado automaticamente após a criação do componente.
  ngOnInit(): void {
    this.loadPlanets();
  }

  loadPlanets(): void {
    this.loading = true;
    this.errorMessage = null;

    this.starWarsService.getPlanets(this.currentPage).subscribe({
      // "next" é chamado quando a resposta HTTP chega com sucesso
      next: (data) => {
        this.planets = data;
        this.loading = false;
        this.changeDetector.markForCheck();
      },
      // "error" é chamado se a API retornar erro ou a rede falhar
      error: (err) => {
        console.error('Erro ao buscar planetas', err);
        this.errorMessage = 'Não foi possível carregar os planetas.';
        this.loading = false;
        this.changeDetector.markForCheck();
      },
    });
  }

  nextPage(): void {
    this.currentPage++;
    this.loadPlanets();
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadPlanets();
    }
  }
}
