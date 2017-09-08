const {expect} = require('chai')

const div = n => d => {
  if (d === 0) throw new Exception('Operation fail')
  return n / d
}

describe('Error Composition', () => {
  describe('Error', () => {
    it('Basic', () => {
      expect(div(10)(5)).to.equal(2)
    })
  })
})
