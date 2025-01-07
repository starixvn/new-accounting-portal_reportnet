import * as numeral from 'numeral';
import * as _ from 'lodash';

interface RomanFormatString {
    n?: string;
    k?: string;
    m?: string;
    b?: string;
}

enum RomanType {
    N,
    K,
    M,
    B
}

const numeralFormatStrings = ['0 a', '0.0 a', '0.00 a', '0.000 a'];

function getRomanType(value: number): RomanType {
    let index = 0;
    value = Math.abs(value);
    while (value >= 1000 && index <= 3) {
        index++;
        value = value / 1000;
    }
    switch (index) {
        case 0:
            return RomanType.N;
        case 1:
            return RomanType.K;
        case 2:
            return RomanType.M;
        case 3:
            return RomanType.B;
    }
}

export class RomanFormatter {
    private romanFormatString: RomanFormatString = {};
    private stepSize: number;
    private stepSizeRomanType: RomanType;

    constructor(values: number[]) {
        const stepSize = values[0] - values[1];
        let nFormatIndex = 0;
        let kFormatIndex = 0;
        let mFormatIndex = 0;
        let bFormatIndex = 0;
        values.forEach(value => {
            const romanType = getRomanType(value);
            switch (romanType) {
                case RomanType.N:
                    nFormatIndex = Math.max(nFormatIndex, this.getFormatIndex(value));
                    break;
                case RomanType.K:
                    kFormatIndex = Math.max(kFormatIndex, this.getFormatIndex(value / 1000));
                    break;
                case RomanType.M:
                    mFormatIndex = Math.max(mFormatIndex, this.getFormatIndex(value / 1000000));
                    break;
                case RomanType.B:
                    bFormatIndex = Math.max(bFormatIndex, this.getFormatIndex(value / 1000000000));
                    break;
            }
        });

        this.romanFormatString.n = numeralFormatStrings[nFormatIndex];
        this.romanFormatString.k = numeralFormatStrings[kFormatIndex];
        this.romanFormatString.m = numeralFormatStrings[mFormatIndex];
        this.romanFormatString.b = numeralFormatStrings[bFormatIndex];
    }

    format(value: number) {
        let formatString = numeralFormatStrings[0];
        const romanType = getRomanType(value);
        switch (romanType) {
            case RomanType.N:
                formatString = this.romanFormatString.n;
                break;
            case RomanType.K:
                formatString = this.romanFormatString.k;
                break;
            case RomanType.M:
                formatString = this.romanFormatString.m;
                break;
            case RomanType.B:
                formatString = this.romanFormatString.b;
                break;
        }

        return this.formatWithString(value, formatString);
    }

    private getFormatIndex(delta: number): number {
        let index = 0;
        while (delta !== _.round(delta) && index <= 3) {
            delta = delta * 10;
            index++;
        }
        return index;
    }

    private formatWithString(value: number, formatString: string) {
        return numeral(value).format(formatString);
    }
}
