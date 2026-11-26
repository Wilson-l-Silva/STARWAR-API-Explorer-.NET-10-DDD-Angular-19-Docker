import { TestBed } from '@angular/core/testing';
import { Subject } from 'rxjs';
import { Planet } from '../../models/planet.model';
import { StarWarsService } from '../../services/star-wars';
import { PlanetListComponent } from './planet-list';

describe('PlanetListComponent', () => {
  it('shows the planets and clears loading when the response arrives', async () => {
    const response = new Subject<Planet[]>();
    TestBed.configureTestingModule({
      imports: [PlanetListComponent],
      providers: [{ provide: StarWarsService, useValue: { getPlanets: () => response } }],
    });
    const fixture = TestBed.createComponent(PlanetListComponent);
    const view = fixture.nativeElement as HTMLElement;
    await fixture.whenStable();
    expect(view.textContent).toContain('Carregando...');

    response.next([{
      id: 1,
      name: 'Tatooine',
      climate: 'arid',
      terrain: 'desert',
      population: '200000',
      isPopulated: true,
    }]);
    response.complete();
    await fixture.whenStable();

    expect(view.textContent).not.toContain('Carregando...');
    expect(view.querySelector('.planet-card h3')?.textContent).toBe('Tatooine');
  });
});
