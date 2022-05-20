from flask_restful import Resource
from http import HTTPStatus
from datetime import date

class DateResource(Resource):
    def get(selfs):
        today=date.today()
        return today,HTTPStatus.OK