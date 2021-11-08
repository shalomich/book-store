
export abstract class FilterComponent {
  public abstract propertyName: string;
  public abstract getValue(): string | null;
  public abstract reset(): void;
}
