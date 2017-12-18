import { Component, Input } from "@angular/core";

@Component({
    selector: 'loading-content',
    templateUrl: './loading.card.component.html',
})

export class LoadingCardComponent {
    @Input() isLoading: boolean;
}