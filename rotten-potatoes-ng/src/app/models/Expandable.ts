export class Expandable<T> {
    public value: T;
    public isExpanded: boolean = false;

    constructor(value: T) {
        this.value = value;
    }
}
