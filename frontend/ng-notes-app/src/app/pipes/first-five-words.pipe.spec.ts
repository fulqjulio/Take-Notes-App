import { FirstFiveWordsPipe } from './first-five-words.pipe';

describe('FirstFiveWordsPipe', () => {
  it('create an instance', () => {
    const pipe = new FirstFiveWordsPipe();
    expect(pipe).toBeTruthy();
  });
});
