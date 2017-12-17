import { CurrencyTrigger } from "./currency-trigger";

export interface PriceTrigger extends CurrencyTrigger {
    Currency: string;
    Operator: string;
    Price: number;
}