export interface Review {
    reviewId: number;
    gameId: number;
    userId: number;
    score: number;
    details: string;
    addDate: Date;
}
