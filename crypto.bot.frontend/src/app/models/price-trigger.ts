import { CurrencyTrigger } from "./currency-trigger";

export class PriceTrigger extends CurrencyTrigger {
    currency: string;
    operator: string;
    price: number;
}