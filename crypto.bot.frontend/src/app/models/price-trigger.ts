import { CurrencyTrigger } from "./currency-trigger";

export class PriceTrigger extends CurrencyTrigger {
    Currency: string;
    Operator: string;
    Price: number;
}