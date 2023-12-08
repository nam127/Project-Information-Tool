import { Pipe, PipeTransform } from '@angular/core';
import { StatusEnum } from '../enums/status-enum';

@Pipe({
  name: 'statusPipe'
})
export class StatusPipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case StatusEnum.NEW:
        return 'NEW';
      case StatusEnum.PLA:
        return 'PLA';
      case StatusEnum.INP:
        return 'INP';
      case StatusEnum.FIN:
        return 'FIN';
      default:
        return 'Unknown';
    }
  }
}
