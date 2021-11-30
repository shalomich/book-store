
export abstract class FilterComponent {
  public abstract propertyName: string;
  public abstract label: string;
  public abstract getValue(): string | null;
  public abstract disable(): void;
  public abstract reset(): void;
}
