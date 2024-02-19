import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'firstFiveWords',
  standalone: true
})
export class FirstFiveWordsPipe implements PipeTransform {

  transform(value: string): string {
    return value.split(' ').slice(0, 5).join(' ') + "...";;
  }

}
