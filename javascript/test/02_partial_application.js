
const {expect} = require('chai')
const { comp, pipe, inc, normal_sum, Composition, sum } = require('../basic.js')

describe('Partial application', () => {
  describe('Anonimous functions', () => {
    it('inc and sum 5 to result with pipe', () => {
      const incAndSum5 = pipe(inc, x => normal_sum(x, 5))
      expect(incAndSum5(3)).to.be.equal(9)
    })

    it('inc and sum 5 to result with Composite', () => {
      Composition.of(3)
          .map(inc)
          .map(x => normal_sum(x, 5))
          .done(x => expect(x).to.be.equal(9))
    })
  })
  describe('Named Partial application', () => {
    it('inc and sum 5 with comp', () => {
      const sum5 = x => normal_sum(x, 5)
      expect(comp(sum5, inc)(3)).to.be.equal(9)
    })
  })
  describe('Curry power', () => {
    it('The same with curry function', () => {
      Composition.of(3)
          .map(inc)
          .map(sum(5))
          .done(x => expect(x).to.be.equal(9))
    })
  })
})
