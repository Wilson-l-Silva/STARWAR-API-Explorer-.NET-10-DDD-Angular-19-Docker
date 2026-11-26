// Interface TypeScript que espelha o PlanetDto do back-end.
// Manter os nomes dos campos idênticos evita mapeamento manual.
export interface Character {
  id: number;
  name: string;
  birthYear: string;
  gender: string;
  homeworldName: string | null;
}
