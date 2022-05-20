from flask import Flask
from flask_cors import CORS
from flask_restful import Api
import os

from Resources.Date import DateResource
from Resources.ProtectedDateResource import ProtectedDateResource
from Resources.Token import TokenResource

def create_app():
    app=Flask(__name__)
    CORS(app)
    register_resources(app)
    return app

def register_resources(app):
    api=Api(app)
    api.add_resource(DateResource,"/Date")
    api.add_resource(ProtectedDateResource,"/ProtectedDate")
    api.add_resource(TokenResource,"/Token")

if __name__=="__main__":
    app=create_app()
    app.run(port=int(os.environ.get("PORT", 8080)),host='0.0.0.0',debug=True)

