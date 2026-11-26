// Interface TypeScript que espelha o PlanetDto do back-end.
// Manter os nomes dos campos idênticos evita mapeamento manual.
export interface Planet {
  id: number;
  name: string;
  climate: string;
  terrain: string;
  population: string;
  isPopulated: boolean;
}
